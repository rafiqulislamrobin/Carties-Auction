'use client'

import { Pagination } from 'flowbite-react'
import React from 'react'

type Props = {
  currentPage: number;
  pageCount: number;
  pageChanged: (page: number) => void;

}
export default function AppPagination({ currentPage, pageCount, pageChanged }: Props) {

  return (
      <div>
        <Pagination
         className="flex-initial space-x-2 text-blue-500 mb-5"
          currentPage={currentPage}
          onPageChange={e => pageChanged(e)}
          totalPages={pageCount}
          layout='pagination'
          showIcons={true}
      />
      </div>
  )
}
