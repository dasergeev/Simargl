hmLoadTopic({
hmKeywords:"",
hmTitle:"StoreTmpGroup- Пример использования",
hmDescription:"void StoreTmpGroup_Example (void) { reference gr ;   gr = NewGroup(1); \/* задание группы объектов *\/   LineSeg (-15, 0, 15, 0, 3); \/* объекты записываются *\/ LineSeg ( 0, -",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">StoreTmpGroup- Пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1724860\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">void StoreTmpGroup_Example (void) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference gr ;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">gr = NewGroup(1); \/* задание группы объектов *\/<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LineSeg (-15, 0, 15, 0, 3); \/* объекты записываются *\/<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LineSeg ( 0, -15, 0, 15, 3); \/* во временный список *\/<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Circle (0, 0, 10, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">EndGroup(); \/* закончить формирование группы *\/<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (Yes_No(Фиксируем группу?)) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">StoreTmpGroup(gr);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}; \/* StoreTmpGroup*\/<\/span><\/p>\n\r"
})
