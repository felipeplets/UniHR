using System;
using System.IO;
using System.Web;
using System.Configuration;

namespace UniHR {
    /// <summary>
    /// Class used to load JS files
    /// </summary>
    public class Script {
        /// <summary>
        /// Registrate a JS file
        /// </summary>
        /// <param name="PsFileName">Name of the file that must be loaded</param>
        public static void RegisterFile(string PsFileName) {
            try {
                if (PsFileName != "") {
                    // Get the page object
                    System.Web.UI.Page oPage = (System.Web.UI.Page)HttpContext.Current.Handler;

                    // TODO: Add debug control

                    //// Add the script
                    string sSessionName = "UNIHRSCRIPT_" + oPage.Request.CurrentExecutionFilePath.ToUpper();

                    if (HttpContext.Current.Session[sSessionName] == null || !HttpContext.Current.Session[sSessionName].ToString().Contains(PsFileName + ",")) {
                        HttpContext.Current.Session[sSessionName] = (HttpContext.Current.Session[sSessionName]!=null?HttpContext.Current.Session[sSessionName].ToString():string.Empty) + PsFileName + ",";
                    }
                }
            } catch (Exception ex) {

                throw ex;
            }
        }

        /// <summary>
        /// Load the combined file to the page
        /// </summary>
        public static void Load() {
            try {
                if (HasFiles()) {

                    // TODO: Add debug control

                    // Get the page object
                    System.Web.UI.Page oPage = (System.Web.UI.Page)HttpContext.Current.Handler;

                    // Tag to add the combined styles
                    string sCombinedFileTag = "\r\n<script src=\"UniHR.ashx?t=type/javascript&amp;f={0}\" type=\"text/javascript\"></script>";
                    sCombinedFileTag = string.Format(sCombinedFileTag, oPage.Request.CurrentExecutionFilePath);

                    // Add the file to the page
                    oPage.Header.Controls.AddAt(0, new System.Web.UI.LiteralControl(sCombinedFileTag));
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Verify if there are files to be loaded
        /// </summary>
        public static bool HasFiles() {
            bool bResult = false;
            try {
                // Get the page object
                System.Web.UI.Page oPage = (System.Web.UI.Page)HttpContext.Current.Handler;

                string sSessionName = "UNIHRSCRIPT_" + oPage.Request.CurrentExecutionFilePath.ToUpper();
                if (!string.IsNullOrEmpty(HttpContext.Current.Session[sSessionName].ToString())) {
                    bResult = true;
                }
            } catch (Exception ex) {
                throw ex;
            }
            return bResult;
        }

    }
}
