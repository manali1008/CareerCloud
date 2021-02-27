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
using static CareerCloud.gRPC.Protos.ApplicantJobApplicationService;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantJobApplicationService : ApplicantJobApplicationServiceBase
    {
        public override Task<ApplicantJobApplication> GetApplicantJobApplication(AppJobIdRequest request, ServerCallContext context)
        {
            var logic = new ApplicantJobApplicationLogic(new EFGenericRepository<ApplicantJobApplicationPoco>());

            ApplicantJobApplicationPoco poco = logic.Get(Guid.Parse(request.Id));

            if(poco == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            return new Task<ApplicantJobApplication>(() => { return TranslateFromPoco(poco); });
        }

        public override Task<Empty> CreateApplicantJobApplication(ApplicantJobApplications request, ServerCallContext context)
        {
            var logic = new ApplicantJobApplicationLogic(new EFGenericRepository<ApplicantJobApplicationPoco>());

            List<ApplicantJobApplicationPoco> pocos = new List<ApplicantJobApplicationPoco>();

            foreach (ApplicantJobApplication proto in request.AppJob)
            {
                pocos.Add(TranslateFromProto(proto));
            }

            logic.Add(pocos.ToArray());

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> UpdateApplicantJobApplication(ApplicantJobApplications request, ServerCallContext context)
        {
            var logic = new ApplicantJobApplicationLogic(new EFGenericRepository<ApplicantJobApplicationPoco>());

            List<ApplicantJobApplicationPoco> pocos = new List<ApplicantJobApplicationPoco>();

            foreach (ApplicantJobApplication proto in request.AppJob)
            {
                pocos.Add(TranslateFromProto(proto));
            }

            logic.Update(pocos.ToArray());

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteApplicantJobApplication(ApplicantJobApplications request, ServerCallContext context)
        {
            var logic = new ApplicantJobApplicationLogic(new EFGenericRepository<ApplicantJobApplicationPoco>());

            List<ApplicantJobApplicationPoco> pocos = new List<ApplicantJobApplicationPoco>();

            foreach(ApplicantJobApplication proto in request.AppJob)
            {
                pocos.Add(TranslateFromProto(proto));
            }

            logic.Delete(pocos.ToArray());

            return Task.FromResult(new Empty());
        }

        private ApplicantJobApplicationPoco TranslateFromProto(ApplicantJobApplication proto)
        {
            return new ApplicantJobApplicationPoco
            {
                Id = Guid.Parse(proto.Id),
                Applicant = Guid.Parse(proto.Applicant),
                Job = Guid.Parse(proto.Job),
                ApplicationDate = Convert.ToDateTime(proto.ApplicationDate)
            };
        }
        private ApplicantJobApplication TranslateFromPoco(ApplicantJobApplicationPoco poco)
        {
            return new ApplicantJobApplication()
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                Job = poco.Job.ToString(),
                ApplicationDate = poco.ApplicationDate == null ? null : Timestamp.FromDateTime((DateTime)poco.ApplicationDate)
            };
        }
    }
}
