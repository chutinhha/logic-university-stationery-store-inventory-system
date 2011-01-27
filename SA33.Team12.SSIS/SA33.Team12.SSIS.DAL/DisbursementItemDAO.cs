﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Transactions;
using System.Data.Objects;

namespace SA33.Team12.SSIS.DAL
{
    public class DisbursementItemDAO:DALLogic
    {
        public DisbursementItem CreateDisbursementItem(DAL.DisbursementItem disbursementItem)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    context.DisbursementItems.AddObject(disbursementItem);
                    context.SaveChanges();
                    ts.Complete();
                    return disbursementItem;
                }
            }
            catch (Exception)
            {
                throw new Exceptions.DisbursmentItemException("Add object not successful");
            }
        }

        public DisbursementItem UpdateDisbursementItem(DAL.DisbursementItem disbursementItem)
        {
            try
            {
                DisbursementItem tempDisbursementItem = (from d in context.DisbursementItems
                                                         where d.DisbursementItemID == disbursementItem.DisbursementItemID
                                                         select d).First<DisbursementItem>();
                tempDisbursementItem.QuantityDisbursed = disbursementItem.QuantityDisbursed;
                using (TransactionScope ts = new TransactionScope())
                {
                    context.Attach(tempDisbursementItem);
                    context.ObjectStateManager.ChangeObjectState(tempDisbursementItem, EntityState.Modified);
                    context.SaveChanges();
                    ts.Complete();
                    return tempDisbursementItem;
                }
            }
            catch
            {
                throw new Exceptions.DisbursmentItemException("Update object not successful");
            }
        }


    }
}
