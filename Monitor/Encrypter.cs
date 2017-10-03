using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monitor
{

    public class Encrypter
    {

        //internal string encrypt_string;
        internal static Encrypter instance;
        internal string ENCRYPTION_KEY = "111111111111111111";
        //internal string ENCRYPTION_KEY = "23840196502610386049";
        internal int ENCRYPTION_INDEX = 0;

        internal Encrypter() { }

        // General case; get itself
        public string EncryptString
        {
            get
            {
                return "";
            }
        }

        // Instantiate Encrypter
        public static Encrypter Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new Encrypter();
                }
                return instance;
            }
        }

        // Return KEY_CHAR_INDEX
        internal int get_encryption_value()
        {
            ENCRYPTION_INDEX++;
            if (ENCRYPTION_INDEX > ENCRYPTION_KEY.Length-1) ENCRYPTION_INDEX = 0;
            return Convert.ToInt32(ENCRYPTION_KEY[ENCRYPTION_INDEX].ToString());
        }

        // Return whether or not char is letter/number/punctuation
        internal bool check_char_validity(string c)
        {
            char d = c[0];
            int unicode = d;
            if ((unicode <= 126 && unicode >= 32) && (d.ToString() != ";") && (d.ToString() != "(") && (d.ToString() != "}") && (d.ToString() != "'"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string Encrypt(string encrypt_string) 
        {
            ENCRYPTION_INDEX = 0;
            string return_string = "";
            int converted, unicode, enc_value;
            foreach (char c in encrypt_string)
            {
                enc_value = get_encryption_value();
                unicode = c;
                converted = unicode + enc_value + 42;
                if (converted > 123 && converted < 160) converted = converted + 42;
                return_string = return_string + Convert.ToChar(converted);
            }
            return return_string;
        }

        public string Decrypt(string encrypt_string)
        {
            ENCRYPTION_INDEX = 0;
            string return_string = "";
            int converted, unicode, enc_value;
            foreach (char c in encrypt_string)
            {
                enc_value = get_encryption_value();
                unicode = c;
                converted = unicode - enc_value - 42;
                if (converted > 123 && converted < 160) converted = converted - 42;
                return_string = return_string + Convert.ToChar(converted);
            }
            return return_string;
        }



        /*
        // Return the string encrypted
        public string Encrypt(string encrypt_string)
        {
            ENCRYPTION_INDEX = 0;
            string return_string = "";
            int converted = 0;
            foreach (char c in encrypt_string)
            {
                int enc_value = get_encryption_value();
                int unicode = c;
                converted = unicode + enc_value;
                if (converted > 126)
                    converted = unicode + enc_value - 94;
                
         * 
         * 
         * 
         * 
         * (!check_char_validity(Convert.ToChar(converted).ToString()))
                {
                    if (converted > 126)
                    {
                        converted = converted - 94;
                    }
                    else
                    {
                        converted = converted + enc_value;
                    }
                }
                return_string = return_string + Convert.ToChar(converted);
            }
            return return_string;
        }


        public string Decrypt(string encrypt_string)
        {
            ENCRYPTION_INDEX = 0;
            string return_string = "";
            int converted = 0;
            foreach (char c in encrypt_string)
            {
                int enc_value = get_encryption_value();
                int unicode = c;
                converted = unicode - enc_value;
                if (converted < 32)
                    converted = unicode - enc_value + 94;
                while (!check_char_validity(Convert.ToChar(converted).ToString()))
                {
                    if (converted < 32)
                    {
                        converted = converted + 94;
                    }
                    else
                    {
                        converted = converted - enc_value;
                    }
                }
                return_string = return_string + Convert.ToChar(converted);
            }
            return return_string;
        }
         */
    }
}
