namespace Event_Hub_API
{
    public class Draft
    {
        try
            {
                User user = database.Users.First(user => user.Email.Equals(UserAttribute.Email));
                if (user != null)
                {
                    if (user.Password.Equals(UserAttribute.Password))
                    {
                        string SecurityKey = "test_segurity_token";
                        var SymmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
                        var UserAttributeAccess new SigningCredentials(SymmetrikKey, SecurityAlgorithms.HmaSha256Signature);

                        var JWT = new JwtSecurityToken(
                            issuer: "EventHub API",
                            expires: DateTimeConstantAttribute.Now.AddHours(1),
                            audience: "user_admin",
                            signingCredentials: UserAttributeAccess
                        );

                        return Ok (new JwtSecurityTokenHandler().WriteToken(JWT));
                    }

                }
            }
            catch (System.Exception)
            {
                
                throw;
            }
    }
}