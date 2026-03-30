hmLoadTopic({
hmKeywords:"",
hmTitle:"IntersectLinSLinS - пример использования",
hmDescription:"void IntersectLinSLinS_Example (void) {   double x, y; int k;   LineSeg ( 20, 20, 100, 100, 1); LineSeg ( 80, 20, 0, 50, 1);   IntersectLinSLinS (20, 20, 100, 100, 80,",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">IntersectLinSLinS - пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1717430\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">void IntersectLinSLinS_Example (void) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">double x, y;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">int k;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LineSeg ( 20, 20, 100, 100, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LineSeg ( 80, 20, 0, 50, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">IntersectLinSLinS (20, 20, 100, 100, 80, 20, 0, 50, &amp;k, &amp;x, &amp;y);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">char buf[128];<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">sprintf(buf, &quot;Количество пересечений = %i&quot;, k);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::Message(buf);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (k) Point (x, y, 1); \/* точка пересечения *\/<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">};<\/span><\/p>\n\r"
})
