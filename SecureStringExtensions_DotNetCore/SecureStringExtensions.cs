using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace SecureStringExtensions_DotNetCore
{
    public unsafe static class SecureStringExtensions
    {
        public static unsafe string GetString(this SecureString sstr)
        {
            StringBuilder sb = new StringBuilder();

            if (sstr == null)
            {
                return String.Empty;
            }

            if (sstr.Length == 0)
            {
                return String.Empty;
            }

            IntPtr bstrPointer = IntPtr.Zero;

            try
            {
                bstrPointer = Marshal.SecureStringToBSTR(sstr);

                char* charPtr = (char*)bstrPointer.ToPointer();

                for (; *charPtr != 0; ++charPtr)
                {
                    sb.Append(*charPtr);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message.ToString());
            }
            finally
            {
                if (bstrPointer != IntPtr.Zero)
                    Marshal.ZeroFreeBSTR(bstrPointer);
            }

            return sb.ToString();
        }

        public static unsafe bool ArePasswordsEqual(this SecureString pass1, SecureString pass2)
        {           
            if (pass1 == null || pass2 == null)
            {
                return false;             
            }

            if (pass1.Length != pass2.Length)
            {
                return false;               
            }

            IntPtr ptrBSTR1 = IntPtr.Zero;

            IntPtr ptrBSTR2 = IntPtr.Zero;

            try
            {
                ptrBSTR1 = Marshal.SecureStringToBSTR(pass1);

                ptrBSTR2 = Marshal.SecureStringToBSTR(pass2);

                char* ptr1 = (char*)ptrBSTR1.ToPointer();

                char* ptr2 = (char*)ptrBSTR2.ToPointer();

                for (; *ptr1 != 0 && *ptr2 != 0; ++ptr1, ++ptr2)
                {
                    if (*ptr1 != *ptr2)
                    {                        
                        return false;
                    }
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine($"{e.Message}");
            }
            finally
            {
                if (ptrBSTR1 != IntPtr.Zero)
                {
                    Marshal.ZeroFreeBSTR(ptrBSTR1);
                }

                if (ptrBSTR2 != IntPtr.Zero)
                {
                    Marshal.ZeroFreeBSTR(ptrBSTR2);
                }
            }

            return true;
        }
    }
}

