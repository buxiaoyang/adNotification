using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices.Protocols;
using System.DirectoryServices;
using System.Collections;

namespace JSYCRM.DAL
{
    public class ldap_cn
    {
        public ArrayList getUserGroupList(String filter)
        {
            DAL.z_parameter dal_z_parameter = new DAL.z_parameter();
            Dictionary<string, string> ldapParameters = (Dictionary<string, string>)dal_z_parameter.GetListOfType("LDAP");
            ArrayList cnList = new ArrayList();
            DirectoryEntry de = new DirectoryEntry(ldapParameters["RootPath"], ldapParameters["LoginUser"], ldapParameters["LoginPassword"]);
            DirectorySearcher ds = new DirectorySearcher(de);
            ds.Filter = "(&((|(" + ldapParameters["TypeAttribute"] + "=" + ldapParameters["UserFilter"] + ")(" + ldapParameters["TypeAttribute"] + "=" + ldapParameters["GroupFilter"] + ")))(" + ldapParameters["UserAccountAttribute"] + "=" + filter + "))";
            ds.SearchScope = System.DirectoryServices.SearchScope.Subtree;
            SearchResultCollection src = ds.FindAll();
            foreach (SearchResult sr in src)
            {
                DirectoryEntry dei = sr.GetDirectoryEntry();

                cnList.Add(deToModel(dei, ldapParameters));
            }
            return cnList;
        }

        public ArrayList getUserGroupInGroup(String group)
        {
            DAL.z_parameter dal_z_parameter = new DAL.z_parameter();
            Dictionary<string, string> ldapParameters = (Dictionary<string, string>)dal_z_parameter.GetListOfType("LDAP");
            ArrayList cnList = new ArrayList();
            DirectoryEntry de = new DirectoryEntry(ldapParameters["RootPath"], ldapParameters["LoginUser"], ldapParameters["LoginPassword"]);
            DirectorySearcher ds = new DirectorySearcher(de);
            ds.Filter = "(&((|(" + ldapParameters["TypeAttribute"] + "=" + ldapParameters["UserFilter"] + ")(" + ldapParameters["TypeAttribute"] + "=" + ldapParameters["GroupFilter"] + ")))(" + ldapParameters["UserMemberOfAttribute"] + "=" + group + "))";
            ds.SearchScope = System.DirectoryServices.SearchScope.Subtree;
            SearchResultCollection src = ds.FindAll();
            foreach (SearchResult sr in src)
            {
                DirectoryEntry dei = sr.GetDirectoryEntry();

                cnList.Add(deToModel(dei, ldapParameters));
            }
            return cnList;
        }

        public ArrayList getUserGroupRoot()
        {
            DAL.z_parameter dal_z_parameter = new DAL.z_parameter();
            Dictionary<string, string> ldapParameters = (Dictionary<string, string>)dal_z_parameter.GetListOfType("LDAP");
            ArrayList cnList = new ArrayList();
            DirectoryEntry de = new DirectoryEntry(ldapParameters["RootPath"], ldapParameters["LoginUser"], ldapParameters["LoginPassword"]);
            DirectorySearcher ds = new DirectorySearcher(de);
            ds.Filter = "(&((|(" + ldapParameters["TypeAttribute"] + "=" + ldapParameters["UserFilter"] + ")(" + ldapParameters["TypeAttribute"] + "=" + ldapParameters["GroupFilter"] + ")))(!(" + ldapParameters["UserMemberOfAttribute"] + "=*)))";
            ds.SearchScope = System.DirectoryServices.SearchScope.Subtree;
            SearchResultCollection src = ds.FindAll();
            foreach (SearchResult sr in src)
            {
                DirectoryEntry dei = sr.GetDirectoryEntry();

                cnList.Add(deToModel(dei, ldapParameters));
            }
            return cnList;
        }

        public Models.ldap_cn deToModel(DirectoryEntry dei, Dictionary<string, string> ldapParameters)
        {
            string type = "";
            if (dei.Properties[ldapParameters["TypeAttribute"]].Value != null)
            {
                if (dei.Properties[ldapParameters["TypeAttribute"]].Contains(ldapParameters["UserFilter"]))
                {
                    type = "user";
                }
                else if (dei.Properties[ldapParameters["TypeAttribute"]].Contains(ldapParameters["GroupFilter"]))
                {
                    type = "group";
                }

            }
            string account = "";
            if (dei.Properties[ldapParameters["UserAccountAttribute"]].Value != null)
            {
                account = dei.Properties[ldapParameters["UserAccountAttribute"]][0].ToString();
            }
            string name = "";
            if (dei.Properties[ldapParameters["UserNameAttribute"]].Value != null)
            {
                name = dei.Properties[ldapParameters["UserNameAttribute"]][0].ToString();
            }
            else
            {
                name = account;
            }
            string email = "";
            if (dei.Properties[ldapParameters["UserEmailAttribute"]].Value != null)
            {
                email = dei.Properties[ldapParameters["UserEmailAttribute"]][0].ToString();
            }

            string path = "";
            if (dei.Properties[ldapParameters["UserPath"]].Value != null)
            {
                path = dei.Properties[ldapParameters["UserPath"]][0].ToString();
            }

            Models.ldap_cn model_ldap_cn = new Models.ldap_cn();
            model_ldap_cn.TYPE = type;
            model_ldap_cn.ACCOUNT = account;
            model_ldap_cn.EMAIL = email;
            model_ldap_cn.NAME = name;
            model_ldap_cn.PATH = path;
            return  model_ldap_cn;
        }

    }
}