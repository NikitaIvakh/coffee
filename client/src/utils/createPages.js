export function createPages(totalCount, pageSize, currentPage) {
	const pagesCount = Math.ceil(totalCount / pageSize)
	const pages = []
	
	if (pagesCount > 10) {
		if (currentPage > 2) {
			for (let i = currentPage - 2; i <= currentPage + 2 && i <= pagesCount; i++) {
				pages.push(i)
			}
			if (currentPage + 2 < pagesCount) {
				pages.push(pagesCount)
			}
		} else {
			for (let i = 1; i <= 5 && i <= pagesCount; i++) {
				pages.push(i)
			}
			if (5 < pagesCount) {
				pages.push(pagesCount)
			}
		}
	} else {
		for (let i = 1; i <= pagesCount; i++) {
			pages.push(i)
		}
	}
	
	return pages
}
