using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RMCodeGen
{
    public class CodeHighLight
    {
           public static string Highlight( string code ) {
                        Regex re;

                        // Replace all strings in green

                        re = new Regex("\"[^\"\\\r\n]*(\\.[^\"\\\r\n]*)*\"", RegexOptions.Singleline );
                        code = re.Replace( code, new MatchEvaluator( StringHandler ) );

                        // Replace comments
                        re = new Regex( @"//.*$", RegexOptions.Multiline );

                        code = re.Replace( code, new MatchEvaluator( CommentHandler ) );
                        re = new Regex( @"/\*.*?\*/", RegexOptions.Singleline );
                        code = re.Replace( code, new MatchEvaluator( CommentHandler ) );

                        // Replace prepocessor commands
                        re = new Regex( @"^\s*#.*", RegexOptions.Multiline );

                        code = re.Replace( code, new MatchEvaluator( PreHandler ) );

                        // Replace keywords
                        string keywords = 
                                    "abstract as base bool break byte case catch char checked " +
                                    "class const continue decimal default delegate do double " +
                                    "else enum event explicit extern false finally fixed " +
                                    "float for foreach goto if implicit in int interface " +
                                    "internal is lock long namespace new null object operator " +
                                    "out override params private protected public readonly " +
                                    "ref return sbyte sealed short sizeof stackalloc static " +
                                    "string struct switch this throw true try typeof uint " +
                                    "ulong unchecked unsafe ushort using virtual void while";

                        keywords = @"\b" + keywords.Replace(    " ", @"\b|\b") + @"\b";
                        re = new Regex( keywords, RegexOptions.Multiline );
                        code = re.Replace( code, new MatchEvaluator( KeywordHandler ) );


                        // Replace tabs
                        re = new Regex( @"^(\t*)", RegexOptions.Multiline );
                        code = re.Replace( code, new MatchEvaluator( TabHandler ) );

                        // Replace newlines
                        re = new Regex( @"^" );

                        code = re.Replace( code, new MatchEvaluator( LineHandler ) );

                        return code;
                }

                private static string TabHandler( Match m ) {
                        return m.Value.Replace(     "\t",     "        " );

                }

                private static string StringHandler( Match m ) {
                        string str = 
                                "<span style=\"color:green;\">" + 
                                m.Value.Replace(    "<",     "<").Replace(    ">",     ">") + 
                                "</span>";
                        return str;
                }

                private static string CommentHandler( Match m ) {
                        string str = 
                                    "<span style=\"color:gray;\">" + 
                                m.Value.Replace(    "<",     "<").Replace(    ">",     ">") + 
                                    "</span>";
                        return str;
                }

                private static string PreHandler( Match m ) {
                        string str = 
                                    "<span style=\"color:red;\">" + 
                                    m.Value.Replace(    "<",     "<").Replace(    ">",     ">") + 
                                    "</span>";

                        return str;
                }

                private static string KeywordHandler(Match m)
                {
                        string str = 
                                    "<span style=\"color:blue;\">" + 
                                    m.Value.Replace(    "<",     "<").Replace(    ">",     ">") + 
                                    "</span>";

                        return str;
                }

                private static string LineHandler(Match m)
                {
                        string str = m.Value +     "<br />";
                        return str;
                }

    }
}
