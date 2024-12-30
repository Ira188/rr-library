using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rrlib481.Models
{
    public interface IAutoReloadDll
    {
        /// <summary>
        /// Worker Instance Id
        /// </summary>
        string InstanceId { get; }
        /// <summary>
        /// Setup the instance
        /// </summary>
        /// <param name="id">Id of the instance</param>
        /// <param name="subdirectory">Subdirectory where the MT5 library files are located</param>
        void Setup(string id, string subdirectory);
        /// <summary>
        /// Start the long running task worker
        /// </summary>
        void Start();
        /// <summary>
        /// Stop Grpc Server
        /// </summary>
        void StopServer();
        /// <summary>
        /// Waiting for the worker to stop
        /// </summary>
        void WaitingForWorkerToStop();
    }
}
