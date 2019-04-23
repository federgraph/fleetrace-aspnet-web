using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RiggVar.FR
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BridgeController : ControllerBase
    {

        // AngularID = 30
        // AngularName = getBackup(): Observable<string[]>
        // GET: api/backup
        // GET: api/Bridge/Backup
        [HttpGet]
        public string[] Backup()
        {
            List<string> SL = new List<string>
            {
                "backup",
                "a",
                "b",
                "c"
            };
            return SL.ToArray();
        }

        // AngularID = 31
        // AngularName = getBacklog(): Observable<string[]>
        // GET: api/backlog
        // GET: api/Bridge/Backlog
        [HttpGet]
        public string[] Backlog()
        {
            return new[] {
            "backlog",
            "helllo",
            "world"
            };
        }

        // AngularID = 32
        // AngularName = getBackupAndLog(): Observable<string[]>
        // GET: api/backlog-and-log
        // GET: api/Bridge/BackupAndLog
        [HttpGet]
        public string[] BackupAndLog()
        {
            return new[]{ "backup-and-log"};
        }

        // AngularID = 33
        // AngularName = getBackupString(): Observable<string>
        // GET: api/backup-string
        // GET: api/Bridge/BackupString
        [HttpGet]
        public string BackupString()
        {
            return "backup-string";
        }

        // AngularID = 34
        // AngularName = getBacklogString(): Observable<string>
        // GET: api/backlog-string
        // GET: api/Bridge/BacklogString
        [HttpGet]
        public string BacklogString()
        {
            return "backlog-string";
        }

        // AngularID = 35
        // AngularName = getBackupAndLogString(): Observable<string>
        // GET: api/backup-and-log-string
        // GET: api/Bridge/BackupAndLogString
        [HttpGet]
        public string BackupAndLogString()
        {
            return "backup-and-log-string";
        }

        // AngularID = 36
        // AngularName = getBackupAndLogJsonString(): Observable<string>
        // GET: api/backup-and-log-json-string
        // GET: api/Bridge/BackupAndLogJsonString
        [HttpGet]
        public string BackupAndLogJsonString()
        {
            return "backup-and-log-json-string";
        }

    }
}