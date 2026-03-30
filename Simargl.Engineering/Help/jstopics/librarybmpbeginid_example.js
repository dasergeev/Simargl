hmLoadTopic({
hmKeywords:"",
hmTitle:"Пример использования функции LibraryBmpBeginID",
hmDescription:"-------------------------------------- \/\/ Получить начало диапазона иконок для команд библиотеки \/\/ --- unsigned int WINAPI LibraryBmpBeginID( unsigned int bmpSizeType ) { int",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">Пример использования функции LibraryBmpBeginID <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><span class=\"f_bodytext\">--------------------------------------<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/ Получить начало диапазона иконок для команд библиотеки<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/ ---<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">unsigned int WINAPI LibraryBmpBeginID( unsigned int bmpSizeType )<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">{<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">int res = 0;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">switch ( bmpSizeType )<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">{<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">case ksBmp1616: res = 1000; break;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">case ksBmp2424: res = 2000; break;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">return res;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r"
})
