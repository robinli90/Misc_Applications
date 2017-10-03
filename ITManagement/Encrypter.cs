using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITManagement
{ 
    class Encrypter : IDisposable
    {
        //internal static Encrypter instance;
        internal string ENCRYPTION_KEY = "11111111111111111";
        internal int ENCRYPTION_INDEX = 0;

        // Instantiate Encrypter
        /*
        public static Encrypter Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new Encrypter();
                }
                return instance 
            }
        }*/

        void IDisposable.Dispose()
        {

        }

        internal int _GET_ENCRYPTION_VALUE()
        {
            int _RET_INC_VALUE = Convert.ToInt32(ENCRYPTION_KEY[ENCRYPTION_INDEX]);
            ENCRYPTION_INDEX++;
            if (ENCRYPTION_INDEX > ENCRYPTION_KEY.Length - 1)
                ENCRYPTION_INDEX = 0;
            return _RET_INC_VALUE;
        }

        public string Encrypt(string encrypt_string)
        {
            string return_string = "";
            if (encrypt_string.Length > 0)
            {
                ENCRYPTION_INDEX = 0;
                try
                {
                    foreach (char c in encrypt_string)
                    {
                        return_string = return_string + Convert.ToChar(c + _GET_ENCRYPTION_VALUE());
                    }
                }
                catch { }
            }
            return return_string;
        }

        public string Decrypt(string encrypt_string)
        {
            
            ENCRYPTION_INDEX = 0;
            string return_string = "";
            try
            {
                foreach (char c in encrypt_string)
                {
                    return_string = return_string + Convert.ToChar(c - _GET_ENCRYPTION_VALUE());
                }
            }
            catch { }
            return return_string;
        }
    }
}
