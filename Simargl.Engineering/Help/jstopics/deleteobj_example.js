hmLoadTopic({
hmKeywords:"",
hmTitle:"DeleteObj - пример использования",
hmDescription:"void DeleteObj_Example (void) {   reference gr ;   gr = NewGroup(0); \/* задание группы объектов *\/   LineSeg (10, 10, 10, 20, 1); \/* объекты записываются *\/ LineSeg (10, 2",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">DeleteObj - пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1725790\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">void DeleteObj_Example (void) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference gr ;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">gr = NewGroup(0); \/* задание группы объектов *\/<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LineSeg (10, 10, 10, 20, 1); \/* объекты записываются *\/<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LineSeg (10, 20, 40, 20, 1); \/* в модель текущего вида *\/<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LineSeg (40, 20, 40, 30, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LineSeg (40, 30, 70, 30, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LineSeg (70, 30, 70, 10, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LineSeg (70, 10, 10, 10, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">EndGroup(); \/* закончить формирование группы *\/<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (Yes_No(Удалять группу?))<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">DeleteObj(gr);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}; \/* DeleteObj_Example *\/<\/span><\/p>\n\r"
})
