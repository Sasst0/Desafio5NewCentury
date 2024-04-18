using NC_AdvinhaNum.Domain.Repository;
using NC_AdvinhaNum.Services.Interface;

namespace NC_AdvinhaNum.Services.AppService
{
    public class AdvinhaNCService : IAdvinhaNCService
    {
        private AdvinhaNCRepository _repository;
        public AdvinhaNCService(AdvinhaNCRepository repository)
        {
            _repository = repository;
        }

     
    }

}
