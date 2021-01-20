using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.util
{
    public class CmdUtil
    {
        //private static Logger LOGGER = LoggerFactory.getLogger(CmdUtil.class);
        private static String TASKLIST = "tasklist";
        private static String KILL = "taskkill /F /IM ";
        private static StreamReader INPUTSTREAMREADER = null;
        private static BufferedStream BUFFEREDREADER = null;

        public bool cmdProcessBuilder(List<String> commandList)
        {
            // init func
            Process process = new Process();
            StreamReader inputStreamReader = null;
            //BufferedStream bufferedreader = null;
            string cmdParameters = "";
            for (int i = 0; i < commandList.Count; i++)
            {
                cmdParameters += " " + commandList[i];
            }
            ProcessStartInfo startInfo = new ProcessStartInfo("CMD.exe", cmdParameters);
            //try
            //{
            //    Process p = new Process();
            //    p.StartInfo = startInfo;
            //    p.Start();
            //}
            //catch { }

            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            //startInfo.RedirectStandardError = true;
            process.StartInfo = startInfo;
            //processBuilder.redirectErrorStream(true); // redirect stderr to stdin
            try
            {
                process.Start();
                //isProcessRunning()
                using (inputStreamReader = process.StandardOutput)
                {
                    //using (bufferedreader = new BufferedStream( inputStreamReader.BaseStream))
                    //{


                    string line = "";
                    while ((line = inputStreamReader.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                        //LOGGER.info(line);
                    }
                    //}
                    inputStreamReader.Close();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                process.WaitForExit();
            }
            catch (ThreadInterruptedException e)
            {
                Thread.CurrentThread.Interrupt();
            }

            process.Close();
            return true;
        }

        public bool server_cmdProcessBuilder(List<String> commandList)
        {
            // init func
            Process process = null;
            StreamReader inputStreamReader = null;
            BufferedStream bufferedreader = null;

            string cmdParameters = "";
            for (int i = 0; i < commandList.Count; i++)
            {
                cmdParameters += " " + commandList[i];
            }
            ProcessStartInfo startInfo = new ProcessStartInfo("CMD.exe", cmdParameters);
            process.StartInfo = startInfo;
            try
            {
                process.Start();
            }
            catch (IOException e)
            {
                //LOGGER.error(new Gson().toJson(e));
            }

            /* Read the output of command prompt */
            inputStreamReader = new StreamReader(process.StandardInput.BaseStream);
            bufferedreader = new BufferedStream(inputStreamReader.BaseStream);

            String line = "";
            try
            {
                //while ((line = bufferedreader.readLine()) != null)
                while ((line = inputStreamReader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    //LOGGER.info(line);
                }
            }
            catch (IOException e)
            {
            }
            try
            {
                process.WaitForExit();
            }
            catch (Exception e)
            {
                //LOGGER.error(new Gson().toJson(e));
                Thread.CurrentThread.Interrupt();
            }

            process.Kill();

            try
            {
                //bufferedreader.close();
                inputStreamReader.Close();
            }
            catch (IOException e)
            {
                //LOGGER.error(new Gson().toJson(e));
            }

            //try
            //{
            //    inputStreamReader.close();
            //}
            //catch (IOException e)
            //{
            //    LOGGER.error(new Gson().toJson(e));
            //}
            return true;
        }

        public void cmdProcessTerminate(String processName)
        {
            // Detect the process run by cmd
            if (isProcessRunning(processName))
            {
                // Kill the process run by windows
                killProcess(processName);
            }
            try
            {
                if (BUFFEREDREADER != null)
                {
                    BUFFEREDREADER.Close();
                }
                if (INPUTSTREAMREADER != null)
                {
                    INPUTSTREAMREADER.Close();
                }
            }
            catch (IOException e)
            {
                //LOGGER.error(new Gson().toJson(e));
            }
        }

        public bool isProcessRunning(String serviceName)
        {
            // Start the Process
            Process process = null;
            try
            {
                process = Process.GetProcessesByName(serviceName)[0];// Runtime.getRuntime().exec(TASKLIST);
                return true;
            }
            catch (IOException e)
            {
                return false;
                //LOGGER.error(new Gson().toJson(e));
            }

            //// Read and list the process run by windows
            //final BufferedReader reader = new BufferedReader(new InputStreamReader(process.getInputStream()));
            //String line;
            //try
            //{
            //    while ((line = reader.readLine()) != null)
            //    {
            //        if (line.contains(serviceName))
            //        {
            //            return true;
            //        }
            //    }
            //}
            //catch (IOException e)
            //{
            //    LOGGER.error(new Gson().toJson(e));
            //}
            //return false;
        }

        protected void killProcess(String serviceName)
        {
            try
            {
                Process.GetProcessesByName(serviceName)[0].Kill();
            }
            catch (IOException e)
            {
                //LOGGER.error(new Gson().toJson(e));
            }
        }

        /**
         * Check process run by cmdUtil
         * 
         * @param processName - the process you want to kill that you create to windows like RecognizeFace.exe
         */
        public bool cmdProcessCheck(String processName)
        {
            // Detect the process run by cmd
            if (isProcessRunning(processName))
            {
                // Kill the process run by windows
                return true;
            }
            return false;
        }
    }
}