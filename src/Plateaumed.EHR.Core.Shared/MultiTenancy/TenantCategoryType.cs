﻿using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using NpgsqlTypes;

namespace Plateaumed.EHR.MultiTenancy
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TenantCategoryType
    {
        [PgName("Public"), EnumMember(Value = "Public")]
        Public,

        [PgName("Private"), EnumMember(Value = "Private")]
        Private
    }
}

