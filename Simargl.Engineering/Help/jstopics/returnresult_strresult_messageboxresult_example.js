hmLoadTopic({
hmKeywords:"",
hmTitle:"ReturnResult, StrResult, MessageBoxResult - пример использования",
hmDescription:"void ReturnResult _Example ( void ) { char s[80];   LineSeg ( 0, 0, 0, 0, 1 ); \/* совпадающие узлы отрезка *\/ if ( ReturnResult ( ) ) { \/* выдать сообщение в специальном окн",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ReturnResult, StrResult, MessageBoxResult - пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1731178\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">void ReturnResult _Example ( void ) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">char s[80];<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LineSeg ( 0, 0, 0, 0, 1 ); \/* совпадающие узлы отрезка *\/<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if ( ReturnResult ( ) ) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/* выдать сообщение в специальном окне *\/<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">MessageBoxResult( );<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">};<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Circle ( 10, 10, 0, 1 ); \/* нулевой радиус окружности *\/<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if ( ReturnResult ( ) ) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">StrResult( s, 80 );<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Error( s );<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}:<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}; \/* ReturnResult *\/<\/span><\/p>\n\r"
})
