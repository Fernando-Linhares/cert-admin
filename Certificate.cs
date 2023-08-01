using System.Security.Cryptography.X509Certificates;

namespace CertAdmin;

public class Certificate
{
   private X509Store _slot;

    public Certificate()
    {
        var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);

        store.Open(OpenFlags.ReadWrite);

        _slot = store;
    }

    public X509Certificate2 Find(int index)
    {
        return _slot.Certificates[index];
    }

    public List<X509Certificate2> List()
    {
        var listageCertificates = new List<X509Certificate2>();

        foreach(var cert in _slot.Certificates)
            listageCertificates.Add(cert);

        return listageCertificates;
    }

    public bool Create(string path, string password)
    {
        try
        {
            var content = File.ReadAllBytes(path);

            var cert = new X509Certificate2(content, password);

            _slot.Add(cert);

            return true;
        }
        catch (System.Exception exp)
        {
            Console.WriteLine(exp.Message);

            return false;
            
            throw;
        }
    }

    public bool Delete(int index)
    {
        try
        {
            var cert = _slot.Certificates[index];

            _slot.Remove(cert);

            return true;
        }
        catch (System.Exception)
        {
            return false;

            throw;
        }
    }

    ~Certificate()
    {
        _slot.Close();
    }
}