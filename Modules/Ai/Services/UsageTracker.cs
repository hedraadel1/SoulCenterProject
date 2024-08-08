using Newtonsoft.Json;
using SoulCenterProject.Modules.Ai.Services;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;


namespace SoulCenterProject.Modules.Ai.Services
{
    public class UsageTracker
    {
        public UsageData data;
        public UsageLimits limits;

        public event EventHandler UsageWarning;
        public event EventHandler MinuteLimitReached;
        private bool IsLimitExceeded()
        {
            if (data.RequestsCount >= limits.RequestsPerMinute ||
                data.TokensCount >= limits.TokensPerMinute ||
                data.RequestsCount >= limits.RequestsPerDay ||
                data.TokensCount >= limits.TokensPerDay)
            {
                return true;
            }

            return false;
        }
        public UsageTracker()
        {
            data = new UsageData();
            limits = new UsageLimits();

            //  تحميل الحدود من الملفّ
            limits.Load();
        }

        public void IncreaseRequests(int count = 1)
        {
            data.Update(count, 0);

            //  التحقق من الحدود  قبل  الحفظ
            if (!IsLimitExceeded())
            {
                SaveSettings();
            }
        }

        public void IncreaseTokens(int count)
        {
            data.Update(0, count);

            //  التحقق من الحدود  قبل  الحفظ
            if (!IsLimitExceeded())
            {
                SaveSettings();
            }
        }

        public void ResetCounters(bool manualReset = false)
        {
            data.Reset();

            if (manualReset)
            {
                data.LastResetTime = DateTime.Now;
            }

            SaveSettings();
        }

        private void CheckLimits(bool isMinuteCheck = false)
        {
            if (isMinuteCheck)
            {
                //  التحقّق من تجاوز حدود الدقيقة
                if (data.RequestsCount >= limits.RequestsPerMinute || data.TokensCount >= limits.TokensPerMinute)
                {
                    OnMinuteLimitReached();
                }
            }
            else
            {
                //  التحقّق من تجاوز حدود اليوم
                if (data.RequestsCount >= limits.RequestsPerDay || data.TokensCount >= limits.TokensPerDay)
                {
                    //  TODO:  إيقاف إرسال أيّ Requests جديدة مؤقّتًا 
                    //  TODO:  عرض رسالة تحذيرية 
                    //  TODO:  تسجيل الحدث في ملفّ Log
                }
                else if ((double)data.RequestsCount / limits.RequestsPerDay >= 0.9 ||
                         (double)data.TokensCount / limits.TokensPerDay >= 0.9)
                {
                    OnUsageWarning();
                }
            }
        }

        //  دوالّ لتنفيذ الـ Events
        protected virtual void OnUsageWarning()
        {
            UsageWarning?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnMinuteLimitReached()
        {
            MinuteLimitReached?.Invoke(this, EventArgs.Empty);
        }

        //  ملاحظة:  عدّلت  SaveSettings()  و  LoadSettings()  عشان  تتعامل  مع  كلا  من  data  و  limits
        private void SaveSettings()
        {
            //  إنشاء  object  يحتوي  على  data  و  limits
            UsageSettings settings = new UsageSettings
            {
                Data = this.data,
                Limits = this.limits
            };

            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText("usage_settings.json", json);
        }

        private void LoadSettings()
        {
            string settingsFilePath = "usage_settings.json";

            if (File.Exists(settingsFilePath))
            {
                string json = File.ReadAllText(settingsFilePath);
                UsageSettings loadedSettings = JsonConvert.DeserializeObject<UsageSettings>(json);

                //  التأكد من عدم  Null  قبل  استخدام  البيانات  
                if (loadedSettings != null && loadedSettings.Data != null && loadedSettings.Limits != null)
                {
                    this.data = loadedSettings.Data;
                    this.limits = loadedSettings.Limits;
                }
            }
        }
    }
     
}
//  Class  جديد  لحفظ  data  و  limits  مع  بعض  في  ملفّ  JSON
public class UsageSettings
{ 
    public UsageData Data { get; set; }
    public UsageLimits Limits { get; set; }
}

