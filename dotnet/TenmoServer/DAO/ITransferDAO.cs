﻿using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        Transfer SendMoney(Transfer transfer);
    }
}