using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Deployment.WindowsInstaller;
using System.IO;

namespace CustomAction2
{
    public class CustomActions
    {
        static String path = null;
        [CustomAction]
        public static ActionResult SaveDriverType(Session session)
        {
            session.Log("Begin CustomAction2");
            string APPLICATIONFOLDER = "APPFOLDER";
            path =
                session.CustomActionData[APPLICATIONFOLDER] + "xdef";

            string DRIVERTYPE = "DRIVERTYPE";
            if (session.CustomActionData.ContainsKey(DRIVERTYPE))
            {
                File.AppendAllText(path,
                    "driverType=" + session.CustomActionData[DRIVERTYPE] + Environment.NewLine); // Environment.NewLine "\n"
                //session.Log("driverType=" + session.CustomActionData[DRIVERTYPE]);
            }
            session.Log("End CustomAction2");
            return ActionResult.Success;
        }
    }
}
