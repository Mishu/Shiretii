using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiOTTDAL.Constants
{
    public class QueryConstants
    {
        #region SMTP

        public const string AppSettingsKey_SMTPServer   = "SMTPServer";
        public const string AppSettingsKey_SMTPUserName = "SMTPUserName";
        public const string AppSettingsKey_SMTPPassword = "SMTPPassword";
        public const string AppSettingsKey_SMTPPort     = "SMTPPort";
        public const string AppSettingsKey_SMTPUseSSL   = "SMTPUseSSL";

        #endregion

        #region Pictures

        public const string AppSettingsKey_PicturesSavePath = "PicturesSavePath";
        public const string AppSettingsKey_PicturesSaveInterval = "PicturesSaveInterval";
        public const string AppSettingsKey_PicturesCompareTolerance = "PicturesCompareTolerance";

        #endregion
    }
}
