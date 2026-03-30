hmLoadTopic({
hmKeywords:"",
hmTitle:"Оптимизация процесса перерисовки в чертежах и фрагментах",
hmDescription:"Если в результате работы приложения нужно перерисовывать изображение в чертеже, то рекомендуется обновлять только его измененную часть, а не весь чертеж целиком, т.к. в случае...",
hmPrevLink:"instruction_acces.html",
hmNextLink:"panel_create.html",
hmParentLink:"cr69365.html",
hmBreadCrumbs:"<a href=\"libraries_create.html\">Создание прикладных библиотек&nbsp;<\/a> &gt; <a href=\"cr69365.html\">Рекомендации по созданию прикладных библиотек&nbsp;<\/a>",
hmTitlePath:"Создание прикладных библиотек  > Рекомендации по созданию прикладных библиотек  > Оптимизация процесса перерисовки в чертежах и фрагментах ",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">Оптимизация процесса перерисовки в чертежах и фрагментах <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><span class=\"f_bodytext\">Если в результате работы приложения нужно перерисовывать изображение в чертеже, то рекомендуется обновлять только его измененную часть, а не весь чертеж целиком, т.к. в случае большого (насыщенного) чертежа, полная его перерисовки может потребовать длительного времени.<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Для перерисовки части изображения чертежа можно использовать функцию ksReDrawDocPartEx. Следует при этом учитывать, что функция обновляет заданную область во всех окнах перерисовываемого документа.<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Для обновления только одного окна документа, можно воспользоваться функцией WinAPI — InvalidateRect. (HWND окна документа можно получить через <\/span><span class=\"f_bodytext\" style=\"text-decoration: underline;\"><a href=\"idocumentframe_gethwnd.html\" class=\"topiclink\">IDocumentFrame::GetHWND<\/a><\/span><span class=\"f_bodytext\">).<\/span><\/p>\n\r<p class=\"p_Z_LOC_TOC_Title\" style=\"border-top: none; border-right: none; border-left: none;\"><span class=\"f_Z_LOC_TOC_Title\">Подразделы:<\/span><\/p>\n\r<p class=\"p_Z_LOC_TOC\"><span class=\"f_Z_LOC_TOC\">(отсутствуют)<\/span><\/p>\n\r"
})
