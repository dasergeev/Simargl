hmLoadTopic({
hmKeywords:"",
hmTitle:"CommandWindow - пример использования",
hmDescription:"void CommandWindow_Example (void) { RequestInfo info; memset(&info, 0, sizeof(info)); info.commands = \"!Окружность !Отрезок \"; info.commands = Объекты   int j=CommandWindow(&",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">CommandWindow - пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><span class=\"f_bodytext\">void CommandWindow_Example (void) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">RequestInfo info;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">memset(&amp;info, 0, sizeof(info));<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">info.commands = &quot;!Окружность !Отрезок &quot;;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">info.commands = Объекты<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">int j=CommandWindow(&amp;info);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">switch (j) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">case 1:<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Circle(10,10,10,1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">break;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">case 2:<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LineSeg(10,10, 20, 10, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">break;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}; \/* CommandWindow_Example *\/<\/span><\/p>\n\r"
})
