namespace cslox.ErrorHandling
{
    static class ExitCode
    {
        // those error code are taken from the UNIX "sysexits.h" header
        // see https://www.freebsd.org/cgi/man.cgi?query=sysexits for reference

        public static readonly int OK = 0;
        public static readonly int USED_INCORRECTLY = 64;
        public static readonly int DATA_ERROR = 65;
        public static readonly int INTERNAL_SOFTWARE_ERROR = 70;
    }
}
