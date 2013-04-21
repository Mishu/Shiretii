using PiOTTDAL.Entities;
using PiOTTDAL.Entities.Attributes;
using PiOTTDAL.Queries.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiOTTDAL.Queries
{
    public class AppSettingsQuery : QueryRunner
    {
        public string GetAppSettingByKey(string key)
        {
            return GetByAttribute<AppSettings>(key, typeof(Name)).Value;
        }
    }
}
