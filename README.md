## CERTIFICATE MANAGER

#### run commands
To list all certificates in device
    
    $ cert-admin list -a

To add one certificate to device repository

    $ cert-admin add -p [FILE_PATH_CERTIFICATE]

To remove the certificate on device

    $ cert-admin remove -i [INDEX OF CERTIFICATE ON DEVICE]

To Help

    $ cert-admin -h