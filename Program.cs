using System;
namespace Pixol
{
    partial class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                if (args.Length > 1)
                {
                    if (args[0] == "-public")
                    {
                        if (args.Length > 2)
                        {
                            if (args[2] == "-likes")
                            {
                                if (args.Length > 3)
                                {
                                    getPublicPhotos(args[1],Convert.ToInt32(args[3]));
                                }
                                else
                                {
                                    Console.WriteLine("WARNING: Argument value was not specified default argument value was used.");
                                }
                            }
                            else
                            {
                                throw new System.InvalidOperationException("Error: Invalid arguments.");
                            }
                        }
                        else
                        {
                            getPublicPhotos(args[1]);
                        }
                    }
                    else if (args[0] == "-private")
                    {
                        if (args.Length > 2)
                        {
                            if (args.Length > 3)
                            {
                                if (args[3] == "-likes")
                                {
                                    if (args.Length > 4)
                                    {
                                        getPrivatePhotos(args[1], args[2], Convert.ToInt32(args[4]));
                                    }
                                    else
                                    {
                                        Console.WriteLine("WARNING: Argument value was not specified default argument value was used.");
                                    }
                                }
                                else
                                {
                                    throw new System.InvalidOperationException("Error: Invalid arguments.");
                                }
                            }
                            else
                            {
                                getPrivatePhotos(args[1], args[2]);
                            }
                        }
                        else
                        {
                            throw new System.InvalidOperationException("Error: Invalid arguments.");
                        }
                    }
                    else
                    {
                        throw new System.InvalidOperationException("Error: Invalid arguments.");
                    }
                }
                else
                {
                    throw new System.InvalidOperationException("Error: Invalid arguments.");
                }
                Console.WriteLine("Downloaded Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.Read();
        }
    }
}
