using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.SecurityLoginsLogService;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginsLogService : SecurityLoginsLogServiceBase
    {
        public override Task<SecurityLoginsLog> GetSecurityLoginsLog(SecLoginsLogIdRequest request, ServerCallContext context)
        {
            var logic = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>());

            SecurityLoginsLogPoco poco = logic.Get(Guid.Parse(request.Id));

            if(poco == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            return new Task<SecurityLoginsLog>(() => { return TranslateFromPoco(poco); });
        }

        public override Task<Empty> CreateSecurityLoginsLog(SecurityLoginsLogs request, ServerCallContext context)
        {
            var logic = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>());

            List<SecurityLoginsLogPoco> pocos = new List<SecurityLoginsLogPoco>();

            foreach (SecurityLoginsLog proto in request.SecLoginsLog)
            {
                pocos.Add(TranslateFromProto(proto));
            }

            logic.Add(pocos.ToArray());

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> UpdateApplcantEducation(SecurityLoginsLogs request, ServerCallContext context)
        {
            var logic = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>());

            List<SecurityLoginsLogPoco> pocos = new List<SecurityLoginsLogPoco>();

            foreach (SecurityLoginsLog proto in request.SecLoginsLog)
            {
                pocos.Add(TranslateFromProto(proto));
            }

            logic.Update(pocos.ToArray());

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteApplcantEducation(SecurityLoginsLogs request, ServerCallContext context)
        {
            var logic = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>());

            List<SecurityLoginsLogPoco> pocos = new List<SecurityLoginsLogPoco>();

            foreach(SecurityLoginsLog proto in request.SecLoginsLog)
            {
                pocos.Add(TranslateFromProto(proto));
            }

            logic.Delete(pocos.ToArray());

            return Task.FromResult(new Empty());
        }

        private SecurityLoginsLogPoco TranslateFromProto(SecurityLoginsLog proto)
        {
            return new SecurityLoginsLogPoco
            {
                Id = Guid.Parse(proto.Id),
                Login = Guid.Parse(proto.Login),
                SourceIP = proto.SourceIP,
                LogonDate = Convert.ToDateTime(proto.LogonDate)
            };
        }
        private SecurityLoginsLog TranslateFromPoco(SecurityLoginsLogPoco poco)
        {
            return new SecurityLoginsLog()
            {
                Id = poco.Id.ToString(),
                Login = poco.Login.ToString(),
                SourceIP = poco.SourceIP,
                LogonDate = poco.LogonDate == null ? null : Timestamp.FromDateTime((DateTime)poco.LogonDate)
            };
        }
    }
}
