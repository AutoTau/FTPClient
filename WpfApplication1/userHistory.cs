using System;
using System.Collections.Generic;
using System.IO;


namespace WpfApplication1
{
    public class userHistory
    {
                /// <summary>
                /// Struct for a single instance of saved login information
                /// <summary>
                /// <param name="HostName"></param>
                /// <param name="UserName"></param>
                /// <param name="Password"></param>
                /// <param name="Port"></param>
                private struct userLog
                {
                    public string HostName;
                    public string UserName;
                    public string Password;
                    public int Port;
                }
                /// <summary>
                /// List for every instance of saved login information
                /// </summary>
                private List<userLog> userLogHistory;
        
                /// <summary>
                /// Read plain text user information
                /// <summary>
                private List<userLog> readUserLogHistory()
                {
                    userLog userInfo = new userLog();
                    List<userLog> userLogHist = new List<userLog>();
                    
                    string[] lines = File.ReadAllLines( Directory.GetCurrentDirectory() + "/userHistory.txt" );
                    foreach (string line in lines)
                    {
                        string[] col = line.Split( new char[] {','} );
                        userInfo.HostName = col[0];
                        userInfo.UserName = col[1];
                        userInfo.Password = col[2];
                        userInfo.Port     = Int32.Parse(col[3]);
                        userLogHistory.Add(userInfo);
                    }
                    return userLogHist;
                }

                /// <summary>
                /// Writes one instance of userLog to disk
                /// <summary>
                /// <param name="HostName"></param>
                /// <param name="UserName"></param>
                /// <param name="Password"></param>
                /// <param name="Port"></param>
                /// <summary>
                private void writeUserLog(string HostName, string UserName, String Password, int Port)
                {
                    string path = "/userHistory.txt";

                    if (!File.Exists(path))
                    {
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            sw.WriteLine(HostName, ',', UserName, ',', Password, ',', Port);
                        }
                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText(path)) 
                        {
                            sw.WriteLine(HostName, ',', UserName, ',', Password, ',', Port);
                        }	
                    }

                }
    }
}