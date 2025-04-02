using Mysqlx.Crud;
using Mysqlx.Expr;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace Livrable_2_psi
{
    internal class ConnexionBDDCuisinier
    {

        /*
         * 
         a faire en sql
         create user if not exists 'superbozo'@'localhost' identified by '123' ;
        grant all on loueur.* to 'superbozo'@'localhost';
--
create user if not exists 'bozo'@'localhost' identified by 'user' ;
        grant select on loueur.location to 'bozo'@'localhost';
--
revoke delete on loueur.location from 'superbozo'@'localhost';
        show grants for 'superbozo'@'localhost';
        show grants for 'bozo'@'localhost';
        show grants for current_user;
show grants;
--
drop user 'bozo'@'localhost';
        drop user 'superbozo'@'localhost';
        select* from mysql.user;
select user, host from mysql.user order by user;
        */

    }
}
