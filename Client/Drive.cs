using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using wsDrive;

namespace gmTemporaryCustomerCreditLimit.Client
{
    
    public class Drive
    {
        #region Drive call

        public static string Post(string username ,string password,string flagData)
        {
            string folderId = string.Empty;
            IprcWebServicesCoordinatorServiceClient service = new();
            ServiceSessionState state =new() ;
            
            try
            {
                
                FormField[] loginData = new FormField[] { new TextField() { Name = "username", Value = username, Culture = "en-GB" }, new TextField() { Name = "password", Value = EncryptPassword(password), Culture = "en-GB" } };
                state = service.Login("WEB", 0, "", loginData);
                bool loggedIn = service.IsLoggedIn(state);

                if (loggedIn)
                    {
                         folderId = service.BlankFormsWebServiceAction(state, flagData);
                          
                    }

                return folderId;
            }


            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                service.Logout(state);
            }
        }
        public static string EncryptPassword(string passwordInput)

        {

            string result = passwordInput;

            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] byteArray = new byte[passwordInput.Length];

            for (int i = 0; i < passwordInput.Length; i++)

            {

                byteArray[i] = (byte)passwordInput[i];

            }


            byte[] digest = md5.ComputeHash(byteArray);

            StringBuilder hexString = new StringBuilder();

            for (int i = 0; i < digest.Length; i++)

            {

                int val = 0xFF & digest[i];

                String hex = val.ToString("x", CultureInfo.InvariantCulture);

                if (hex.Length == 1)

                {

                    hexString.Append("0" + hex);

                }

                else

                {

                    hexString.Append(hex);

                }

            }



            result = hexString.ToString().ToUpper(CultureInfo.InvariantCulture);


            return result;

        }
        #endregion

       
    }

}
