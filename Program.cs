namespace CertAdmin;

public class Program
{
    public static void Main(string[] args)
    {
        string command = (args.Length > 0)? args[0] : "";

        string argumentName = (args.Length > 1)? args[1] : "";

        string argumentValue = (args.Length > 2)? args[2] : "";

        string result = FlushCommand(command, argumentName, argumentValue);

        Console.WriteLine(result);
    }

    public static string FlushCommand(string command, string argumentName, string argumentValue)
    {
        var certificate = new Certificate();

        return command switch
        {
            "add" => Add(certificate, argumentName, argumentValue),
            "remove" => Remove(certificate, argumentName, argumentValue),
            "list" => List(certificate , argumentName),
            "-h" => Help(),
            _ => NotFoundCommand()
        };
    }

    public static string Help()
    {
        return "COMMANDS TO CERT_ADMIN"
        + "\n cert-admin list -a \t| LIST ALL CERTIFICATES IN DEVICE"
        + "\n cert-admin add -p [FILE_PATH_CERTIFICATE] \t| ADD ONE CERTIFICATE TO DEVICE REPOSITORY"
        + "\n cert-admin remove -i [INDEX_CERTIFICATE] \t| REMOVE CERTIFICATE TO DEVICE REPOSITORY" 
        ;
    }

    public static string Add(Certificate certificate, string argumentName, string argumentValue)
    {
        if(argumentName.Equals("-p"))
        {
            Console.WriteLine("INSERT THE PASSWORD BELLOW");

            string? password = Console.ReadLine();

            if(password is null)
                return "Password Can't be null";

            certificate.Create(argumentValue, password);

            return "Certificated added";
        }

        return "Fail in add certificate";
    }

    public static string Remove(Certificate certificate, string argumentName, string argumentValue)
    {
        if(certificate.Delete(int.Parse(argumentValue)) && argumentName.Equals("-i"))
            return "Certificat remove";

        return "Fail in remove certificate";
    }

    public static string List(Certificate certificate, string argumentName)
    {
        string content = "";

        int i = 0;

        foreach(var cert in certificate.List())
        {

            var raw = $"\n\t{i}  -  {cert.Issuer}\n";

            content += raw;


            i++;
        }

        if(argumentName.Equals("-a"))
            return content;

        return "Fail in add certificate";
    }


    public static string NotFoundCommand()
    {
        return "Command not found";
    }
}