using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using TaskHelper;
using TaskHelper.Model;

namespace FileClearer
{
    public class Clearer : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            //Console.WriteLine("hello");
            /*TemplateRep rep = new TemplateRep("server=localhost;user=root;database=generator;password=;charset=utf8;");
            List<StudentTask> tasks = rep.GetOldTask();
            List<string> files = new List<string>();

            string rootPath = Server.MapPath("~");
            string maketFolder = WebConfigurationManager.AppSettings["MaketFolder"];
            foreach (string task in tasks)
            {
                files.Add(string.Format(@"{0}\{1}\{2}", rootPath, maketFolder, task));
            }

            List<string> solves = rep.GetSolveFromTemplate(template.Id_template);
            string solveFolder = WebConfigurationManager.AppSettings["SolveFolder"];
            foreach (string solve in solves)
            {
                files.Add(string.Format(@"{0}\{1}\{2}", rootPath, solveFolder, solve));
            }

            string templateFolder = WebConfigurationManager.AppSettings["TemplateFolder"];
            files.Add(string.Format(@"{0}\{1}\{2}", rootPath, templateFolder, template.template));

            rep.Delete(template.Id_template);

            foreach (string file in files)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
       */
        }
        public class FileClearManager
        {
            public static void Start()
            {
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
                scheduler.Start();

                IJobDetail job = JobBuilder.Create<Clearer>().Build();

                ITrigger trigger = TriggerBuilder.Create()  // создаем триггер
                    .WithIdentity("trigger1", "group1")     // идентифицируем триггер с именем и группой
                    .StartNow()                            // запуск сразу после начала выполнения
                    .WithSimpleSchedule(x => x            // настраиваем выполнение действия
                        .WithInterval(new TimeSpan(0, 0, 0, 1))          // через 1 минуту
                        .RepeatForever())                   // бесконечное повторение
                    .Build();                               // создаем триггер

                scheduler.ScheduleJob(job, trigger);        // начинаем выполнение работы
            }
        }
    }
}
