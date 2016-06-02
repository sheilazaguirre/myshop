using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Util
/// </summary>
public class Util
{
    public Util()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string GetConnection()
    {
        return ConfigurationManager.ConnectionStrings["MyCon"].ConnectionString;
    }
}