using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Auth.Methods
{
    public class AuthenticationResponse
    {
        public enum ErrorCode
        {
            SUCCESS = 0,
            ERROR_LOGIN_FAILED = 1,
            ERROR_VERSION_MISMATCH = 2,
            ERROR_VERSION_NEWER = 3,
        }

        public bool Success;
        public ErrorCode ErrCode;

        public AuthenticationResponse(byte response)
        {
            ErrCode = (ErrorCode)(response);
            Success = response == 0;
        }
    }
}