using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Configuration;

public class CommonRepositoryBase : IDisposable
{
    private CommonContext _ctx;

    public CommonContext Context
    {
        get
        {
            if (_ctx == null)
            {
                _ctx = new CommonContext();
                _ctx.ContextOptions.LazyLoadingEnabled = true;
            }
            return _ctx;
        }
    }

    private bool disposedValue; // To detect redundant calls

    // IDisposable
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposedValue)
        {
            if (disposing)
            {
            }
        }
        this.disposedValue = true;
    }

    // TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    // Protected Overrides Sub Finalize()
    // ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    // Dispose(False)
    // MyBase.Finalize()
    // End Sub

    // This code added by Visual Basic to correctly implement the disposable pattern.
    public void Dispose()
    {
        // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
