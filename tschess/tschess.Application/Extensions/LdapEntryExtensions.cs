using Novell.Directory.Ldap;


namespace Tschess.Application.Extensions
{
    public static class LdapEntryExtensions
    {
        public static LdapAttribute? TryGetAttribute(this LdapEntry conn, string attribute)
        {
            try
            {
                return conn.GetAttribute(attribute);
            }
            catch
            {
                return null;
            }
        }
    }
}
