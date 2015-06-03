using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Deployment.WindowsInstaller;
using System.IO;
using Ionic.Zip;

namespace CustomAction1
{
    public class CustomActions
    {
        static String path = null;// @"c:\Users\bjcheny\Desktop\config.ini";
        private static void SaveToConfig(Session session, string key)
        {
            session.Log(key + ": " + session.CustomActionData[key]);
            if (session.CustomActionData.ContainsKey(key)
                && session.CustomActionData[key] != "")
            {
                if (session.CustomActionData[key] == "1")
                {
                    File.AppendAllText(path,
                        key + "=true" + Environment.NewLine);
                }
                else
                {
                    File.AppendAllText(path, 
                        key + "=" + session.CustomActionData[key] + Environment.NewLine);
                }
            }
        }

        [CustomAction]
        public static ActionResult CustomAction1(Session session)
        {
            session.Log("Begin CustomAction1 " + DateTime.Now.ToString());
            // File.AppendAllText(@"c:\Users\bjcheny\Desktop\time.txt", ";Installation: " + DateTime.Now.ToString());

            //string appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            //if (!Directory.Exists(appdataPath + "\\Win App"))
            //    Directory.CreateDirectory(appdataPath + "\\Win App");
            //session.Log(appdataPath + "\\Win App\\registrationInfo.txt ");
            // File.AppendAllText(@appdataPath + "\\Win App\\registrationInfo.txt", name + "," + companycy, Encoding.UTF8);
            string APPLICATIONFOLDER = "APPFOLDER";
            path =
                session.CustomActionData[APPLICATIONFOLDER] + "xdef";
            File.WriteAllText(path, string.Empty); // clear content of config file before writing

            string INTERCEPTIONFLAG = "isInterception";
            SaveToConfig(session, INTERCEPTIONFLAG);

            string DATASYNCFLAG = "isDataSync";
            SaveToConfig(session, DATASYNCFLAG);

            string CAPTURESQLFLAG = "isCaptureSql";
            SaveToConfig(session, CAPTURESQLFLAG);

            string CAPTURERESULTFLAG = "isCaptureResult";
            SaveToConfig(session, CAPTURERESULTFLAG);

            string DRIVERID = "driverID";
            SaveToConfig(session, DRIVERID);

            string MIDWAREIP = "midWare";
            SaveToConfig(session, MIDWAREIP);

            string HEARTBEAT = "heartbeat";
            SaveToConfig(session, HEARTBEAT);

            string JDBCFolder = "JDBCFolder";
            string JdbcPath = session.CustomActionData[JDBCFolder];
            //TextReader tr = new StreamReader(@JdbcPath + "JDBC.txt");
            //string myText = tr.ReadLine();

            //string JDBCFLAG = "JDBCFLAG";
            //string ODBCFLAG = "ODBCFLAG";
            //if (session.CustomActionData.ContainsKey(JDBCFLAG)
            //    && session.CustomActionData[JDBCFLAG] == "1")
            //{
            //    File.AppendAllText(path, 
            //        "driverType=" + "jdbc" + Environment.NewLine);
            //}
            //else if (session.CustomActionData.ContainsKey(ODBCFLAG)
            //    && session.CustomActionData[ODBCFLAG] == "1")
            //{
            //    File.AppendAllText(path,
            //        "driverType=" + "odbc" + Environment.NewLine);
            //}

            string myOtherZip = @JdbcPath + "smt-driver5-1.0.jar";
            using (ZipFile zipDest = ZipFile.Read(myOtherZip))
            {
                //zipDest.AddFiles(System.IO.Directory.EnumerateFiles(outputDirectory)); //This will add them as a directory
                zipDest.AddFiles(new string[] { path }, false, ""); //This will add the files to the root
                zipDest.Save();
            }
            session.Log("End CustomAction1 " + session.CustomActionData.ToString());
        
            return ActionResult.Success;
        }
    }
}
