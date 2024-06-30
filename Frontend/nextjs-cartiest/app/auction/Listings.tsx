'use client'

import React, { useEffect, useState } from "react";
import AuctionCard from "./AuctionCard";
import { Auction, PagedResult  } from "@/types";
import AppPagination from "../components/AppPagination";
import { getData } from "../actions/AuctionActions";



export default function Listings() {

  const [auction, setAuction] = useState<Auction[]>([]);
  const [pageCount, setPageCount] = useState(0);
  const [pageNumber, setPageNumber] = useState(1);

  useEffect(() => {
    getData(pageNumber).then(data => {
      setAuction(data.results);
      setPageCount(data.pageCount);
    })
}, [pageNumber])

if(auction.length == 0) return <h3>....Loading</h3>

  return (
    <>
      <div className="grid grid-cols-4 gap-6">
        {auction.map((auction) => (
            <AuctionCard auction={auction} key={auction.id} />
          ))}
      </div>
      <div className="flex justify-center mt-4">
        <AppPagination pageChanged={setPageNumber} currentPage={1} pageCount={pageCount} />
      </div>
    </>
  );
}
