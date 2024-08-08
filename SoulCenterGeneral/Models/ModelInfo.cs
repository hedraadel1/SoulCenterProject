//-----------------------------------------------------------------------
// <copyright file="ModelInfo.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SoulCenterProject.Models
{
    public class ModelInfo
    {
        /// <summary>
        /// Required. The name of the base model, pass this to the generation request. Examples: chat-bison
        /// </summary>
        public string BaseModelId { get; set; }

        /// <summary>
        /// A short description of the model.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The human-readable name of the model. E.g. "Chat Bison". The name can be up to 128 characters long and can
        /// consist of any UTF-8 characters.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Maximum number of input tokens allowed for this model.
        /// </summary>
        public int InputTokenLimit { get; set; }

        /// <summary>
        /// Required. The resource name of the Model. Format: models/{model} with a {model} naming convention of:
        /// "{baseModelId}-{version}" Examples: models/chat-bison-001
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Maximum number of output tokens available for this model.
        /// </summary>
        public int OutputTokenLimit { get; set; }

        /// <summary>
        /// The model's supported generation methods. The method names are defined as Pascal case strings, such as
        /// generateMessage which correspond to API methods.
        /// </summary>
        public string[] SupportedGenerationMethods { get; set; }

        /// <summary>
        /// Controls the randomness of the output. Values can range over [0.0,1.0], inclusive. A value closer to 1.0
        /// will produce responses that are more varied, while a value closer to 0.0 will typically result in less
        /// surprising responses from the model. This value specifies default to be used by the backend while making the
        /// call to the model.
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// For Top-k sampling. Top-k sampling considers the set of topK most probable tokens. This value specifies
        /// default to be used by the backend while making the call to the model. If empty, indicates the model doesn't
        /// use top-k sampling, and topK isn't allowed as a generation parameter.
        /// </summary>
        public int? TopK { get; set; } // nullable integer for optional topK

        /// <summary>
        /// For Nucleus sampling. Nucleus sampling considers the smallest set of tokens whose probability sum is at
        /// least topP. This value specifies default to be used by the backend while making the call to the model.
        /// </summary>
        public double TopP { get; set; }

        /// <summary>
        /// Required. The version number of the model. This represents the major version.
        /// </summary>
        public string Version { get; set; }
    }
}