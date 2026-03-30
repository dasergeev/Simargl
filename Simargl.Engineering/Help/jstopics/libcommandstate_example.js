hmLoadTopic({
hmKeywords:"",
hmTitle:"Пример использования функции LibCommandState",
hmDescription:"\/\/-------------------------------------------------------------------------------",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">Пример использования функции LibCommandState <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/-------------------------------------------------------------------------------<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/ Состояние команды<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/ ---<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">int WINAPI LibCommandState( unsigned int comm, int * enable, int * checked )<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">{<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if ( enable )<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">{<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">int type = ksGetDocumentType( 0 );<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">*enable = type == lt_DocSheetStandart || type == lt_DocFragment || type == lt_DocSheetUser; \/\/ Команда доступна в 2D документе<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">return 0;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r"
})
