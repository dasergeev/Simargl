hmLoadTopic({
hmKeywords:"",
hmTitle:"ksReDrawDocPart - пример использования",
hmDescription:"reference pView = OpenView ( 0 ); \/\/ Системный вид текущего документа",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ksReDrawDocPart - пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcq1943453\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference pView = OpenView ( 0 ); \/\/ Системный вид текущего документа<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">RectParam rect; \/\/ Структура параметров прямоугольника<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">rect.pBot.x = 100; \/\/ Инициализация структуры pBot низ - лево<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">rect.pBot.y = 100; \/\/ pTop верх - право<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">rect.pTop.x = 200;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">rect.pTop.y = 200;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">ksReDrawDocPart( &amp;rect, pView ); \/\/ Будет перерисована область во всех граф. окнах документа<\/span><\/p>\n\r"
})
