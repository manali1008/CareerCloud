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
using static CareerCloud.gRPC.Protos.CompanyJobEducationService;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobEducationService : CompanyJobEducationServiceBase
    {
        public override Task<CompanyJobEducation> GetCompanyJobEducation(CompJobEduIdRequest request, ServerCallContext context)
        {
            var logic = new CompanyJobEducationLogic(new EFGenericRepository<CompanyJobEducationPoco>());

            CompanyJobEducationPoco poco = logic.Get(Guid.Parse(request.Id));

            if(poco == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            return new Task<CompanyJobEducation>(() => { return TranslateFromPoco(poco); });
        }

        public override Task<Empty> CreateCompanyJobEducation(CompanyJobEducations request, ServerCallContext context)
        {
            var logic = new CompanyJobEducationLogic(new EFGenericRepository<CompanyJobEducationPoco>());

            List<CompanyJobEducationPoco> pocos = new List<CompanyJobEducationPoco>();

            foreach (CompanyJobEducation proto in request.CompJobEdu)
            {
                pocos.Add(TranslateFromProto(proto));
            }

            logic.Add(pocos.ToArray());

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> UpdateApplcantEducation(CompanyJobEducations request, ServerCallContext context)
        {
            var logic = new CompanyJobEducationLogic(new EFGenericRepository<CompanyJobEducationPoco>());

            List<CompanyJobEducationPoco> pocos = new List<CompanyJobEducationPoco>();

            foreach (CompanyJobEducation proto in request.CompJobEdu)
            {
                pocos.Add(TranslateFromProto(proto));
            }

            logic.Update(pocos.ToArray());

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteApplcantEducation(CompanyJobEducations request, ServerCallContext context)
        {
            var logic = new CompanyJobEducationLogic(new EFGenericRepository<CompanyJobEducationPoco>());

            List<CompanyJobEducationPoco> pocos = new List<CompanyJobEducationPoco>();

            foreach(CompanyJobEducation proto in request.CompJobEdu)
            {
                pocos.Add(TranslateFromProto(proto));
            }

            logic.Delete(pocos.ToArray());

            return Task.FromResult(new Empty());
        }

        private CompanyJobEducationPoco TranslateFromProto(CompanyJobEducation proto)
        {
            return new CompanyJobEducationPoco
            {
                Id = Guid.Parse(proto.Id),
                Job = Guid.Parse(proto.Job),
                Major = proto.Major,
                Importance =(byte) proto.Importance
            };
        }
        private CompanyJobEducation TranslateFromPoco(CompanyJobEducationPoco poco)
        {
            return new CompanyJobEducation()
            {
                Id = poco.Id.ToString(),
                Job = poco.Job.ToString(),
                Major = poco.Major,
                Importance = Convert.ToInt32(poco.Importance)
            };
        }
    }
}
