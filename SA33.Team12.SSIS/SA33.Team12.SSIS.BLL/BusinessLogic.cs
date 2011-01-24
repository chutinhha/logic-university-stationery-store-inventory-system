/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.BLL
{
    public abstract class BusinessLogic : IDisposable
    {
        protected StationeryEntities context;

        protected BusinessLogic()
        {
            this.context = new StationeryEntities();
        }

        ~BusinessLogic()
        {
            Dispose(false);
        }

        #region Implementation for IDisposable
        private bool disposed = false;
        public virtual void Dispose()
        {
            this.context.Dispose();
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
                throw new ObjectDisposedException(this.ToString());
            if (disposing)
            {
                // dispose managed resource
            }
            disposed = true;
        }
        #endregion
 
    }
}