using CXE.CoreFx.Base.Models;
using CXE.CoreFx.Plugin;

namespace CXE.Sample.Account
{

    [
        CrmPluginRegistration(
         MessageNameEnum.Update,
         "account",
         StageEnum.PostOperation,
         ExecutionModeEnum.Synchronous,
         "msdyn_recordstage",
         "CXE.Sample.Account - On Update Account PostSync - Set Department",
         1,
         IsolationModeEnum.Sandbox
     )
        ]
    public class AccountUpdateSetContactDeptPlugin : PluginBase
    {
        private AccountService _accountService;
        public override void InitializeService() => _accountService = new AccountService(
            this.ServiceClient,
            new AccountModel()
            {
                Id = this.TargetEntity.Id,
                LogicalName = this.TargetEntity.LogicalName,
                Attributes = this.TargetEntity.Attributes
            },
            this.Logger);
    }
}
