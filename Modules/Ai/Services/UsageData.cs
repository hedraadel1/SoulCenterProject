using System;
using System.IO;
using Newtonsoft.Json;

namespace SoulCenterProject.Modules.Ai.Services
{


    public class UsageData
    {
        public int RequestsCount { get; private set; }
        public int TokensCount { get; private set; }
        public DateTime LastRequestTime { get; private set; }
        public DateTime LastResetTime { get; set; }
        public UsageData()
        {
            LastResetTime = DateTime.Today;
        }

        public void Update(int requests, int tokens)
        {
            RequestsCount += requests;
            TokensCount += tokens;
            LastRequestTime = DateTime.Now;
        }

        //  دالة اختيارية لإعادة ضبط البيانات يدويًا
        public void Reset()
        {
            RequestsCount = 0;
            TokensCount = 0;
            LastRequestTime = DateTime.MinValue;
            LastResetTime = DateTime.Now;
        }
    }

    public class UsageLimits
    {
        public int RequestsPerMinute { get; set; } = 2;
        public int RequestsPerDay { get; set; } = 60;
        public int TokensPerMinute { get; set; } = 20000;
        public int TokensPerDay { get; set; } = 100000;

        //  مسار ملفّ JSON لحفظ البيانات (ممكن تغيّره)
        private readonly string filePath = "usage_limits.json";

        public void Save()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public void Load()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                UsageLimits loadedLimits = JsonConvert.DeserializeObject<UsageLimits>(json);

                //  تحديث الخصائص
                RequestsPerMinute = loadedLimits.RequestsPerMinute;
                RequestsPerDay = loadedLimits.RequestsPerDay;
                TokensPerMinute = loadedLimits.TokensPerMinute;
                TokensPerDay = loadedLimits.TokensPerDay;
            }
            //  لو الملفّ مش موجود، هيتمّ استخدام القيم الافتراضية
        }
    }

  
}
