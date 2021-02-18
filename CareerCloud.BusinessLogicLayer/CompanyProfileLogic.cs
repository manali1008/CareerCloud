using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic: BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
        {

        }

        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach(CompanyProfilePoco poco in pocos)
            {
                if(string.IsNullOrEmpty(poco.CompanyWebsite) || !(poco.CompanyWebsite.EndsWith(".ca") || poco.CompanyWebsite.EndsWith(".com") || poco.CompanyWebsite.EndsWith(".biz")))
                {
                    exceptions.Add(new ValidationException(600, "Company name must ends with following extensions .ca, .com, .biz: CompanyProfilePoco"));
                }

                if (string.IsNullOrEmpty(poco.ContactPhone) || !(Regex.Match(poco.ContactPhone, @"[0-9]{3}-[0-9]{3}-[0-9]{4}").Success))
                {
                    exceptions.Add(new ValidationException(601, "Contact phone must be in 000-000-0000 format: CompanyProfilePoco"));
                }

                if(exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }
    }
}
