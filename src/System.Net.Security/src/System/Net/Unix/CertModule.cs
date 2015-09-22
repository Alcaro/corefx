// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace System.Net
{   
    internal partial class CertModule : CertInterface
    {
        #region internal Methods
        internal override SslPolicyErrors VerifyRemoteCertName(X509Chain chain, bool isServer, string hostName)
        {
            SslPolicyErrors sslPolicyErrors = SslPolicyErrors.None;         

            return sslPolicyErrors;
        }

        //
        // Extracts a remote certificate upon request.
        //
        internal override X509Certificate2 GetRemoteCertificate(SafeDeleteContext securityContext, out X509Certificate2Collection remoteCertificateStore)
        {
            remoteCertificateStore = null;

            if (securityContext == null)
            {
                return null;
            }

            GlobalLog.Enter("SecureChannel#" + Logging.HashString(this) + "::RemoteCertificate{get;}");
            X509Certificate2 result = null;
            SafeFreeCertContext remoteContext = null;
            try
            {
                int errorCode = SSPIWrapper.QueryContextRemoteCertificate(GlobalSSPI.SSPISecureChannel, securityContext, out remoteContext);
                if (remoteContext != null && !remoteContext.IsInvalid)
                {
                    result = new X509Certificate2(remoteContext.DangerousGetHandle());
                }
            }
            finally
            {
				//TODO: Fetch remoteCertificateStore, if applicable for unix
				remoteCertificateStore = new X509Certificate2Collection();
            }

            if (Logging.On)
            {
                Logging.PrintInfo(Logging.Web, SR.Format(SR.net_log_remote_certificate, (result == null ? "null" : result.ToString(true))));
            }

            GlobalLog.Leave("SecureChannel#" + Logging.HashString(this) + "::RemoteCertificate{get;}", (result == null ? "null" : result.Subject));

            return result;
        }      

        //
        // Used only by client SSL code, never returns null.
        //
        internal override string[] GetRequestCertificateAuthorities(SafeDeleteContext securityContext)
        {
			//TODO populate issuers
            string[] issuers = Array.Empty<string>();          

            return issuers;
        }

        #endregion
     
    }

}