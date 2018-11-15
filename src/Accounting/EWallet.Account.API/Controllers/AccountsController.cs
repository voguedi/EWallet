using System;
using System.Reflection;
using System.Threading.Tasks;
using EWallet.Accounting.Commands;
using Microsoft.AspNetCore.Mvc;
using Voguedi.Commands;
using Voguedi.IdentityGeneration;

namespace EWallet.Account.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        #region Private Fields

        readonly ICommandSender commandSender;
        readonly IStringIdentityGenerator identityGenerator;

        #endregion

        #region Ctors

        public AccountsController(ICommandSender commandSender, IStringIdentityGenerator identityGenerator)
        {
            this.commandSender = commandSender;
            this.identityGenerator = identityGenerator;
        }

        #endregion

        #region Public Methods

        [HttpPost("create")]
        public async Task<IActionResult> Create(string owner)
        {
            if (string.IsNullOrWhiteSpace(owner))
                return BadRequest(new ArgumentNullException(nameof(owner)));

            var result = await commandSender.SendAsync(new CreateAccountCommand(identityGenerator.Generate(), owner));

            if (result.Succeeded)
                return NoContent();

            return BadRequest(result.Exception);
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit(string accountId, double amount)
        {
            if (string.IsNullOrWhiteSpace(accountId))
                return BadRequest(new ArgumentNullException(nameof(accountId)));

            if (amount <= 0D)
                return BadRequest(new ArgumentOutOfRangeException(nameof(amount)));

            var result = await commandSender.SendAsync(new CommitDepositTransactionCommand(identityGenerator.Generate(), accountId, amount));

            if (result.Succeeded)
                return NoContent();

            return BadRequest(result.Exception);
        }

        #endregion
    }
}