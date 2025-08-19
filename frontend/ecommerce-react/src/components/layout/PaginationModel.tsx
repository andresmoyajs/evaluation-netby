import {
  Pagination,
  PaginationContent,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from "@/components/ui/pagination";
import type { NavigateFunction } from "react-router-dom";

export default function PaginationModel({
  search,
  page,
  pageIndex,
  pageCount,
  totalPages,
  nextPage,
  prevPage,
  navigate,
}: {
  search: string;
  page: number;
  pageIndex: number;
  pageCount: number;
  totalPages: number;
  nextPage: () => void;
  prevPage: () => void;
  navigate: NavigateFunction;
}) {
  return (
    <>
      {!search && (
        <Pagination className="mt-8">
          <PaginationContent>
            <PaginationItem>
              <PaginationPrevious
                className={`${
                  pageIndex === 1 ? "opacity-50 cursor-not-allowed" : ""
                }`}
                onClick={prevPage}
              />
            </PaginationItem>

            {new Array(totalPages).fill("_").map((_, i) => (
              <PaginationItem key={i}>
                <PaginationLink
                  isActive={i + 1 === page}
                  onClick={() => navigate(`/productos/${i + 1}`)}
                >
                  {i + 1}
                </PaginationLink>
              </PaginationItem>
            ))}

            <PaginationItem>
              <PaginationNext
                onClick={nextPage}
                className={`${
                  pageIndex === pageCount
                    ? "opacity-50 cursor-not-allowed"
                    : ""
                }`}
              />
            </PaginationItem>
          </PaginationContent>
        </Pagination>
      )}
    </>
  );
}
