import { useState, useEffect } from "react";
import { Input } from "@/components/ui/input";
import { Button } from "../ui/button";

interface ProductSearchProps {
  onSearch: (search: string) => void;
  setShowAdvanced: React.Dispatch<React.SetStateAction<boolean>>;
  showAdvanced: boolean;
  priceMin: number | "";
  setPriceMin: React.Dispatch<React.SetStateAction<number | "">>;
  priceMax: number | "";
  setPriceMax: React.Dispatch<React.SetStateAction<number | "">>;
}

export default function ProductSearch({
  onSearch,
  setShowAdvanced,
  showAdvanced,
  priceMin,
  setPriceMin,
  priceMax,
  setPriceMax
}: ProductSearchProps) {
  const [search, setSearch] = useState("");
  const [debouncedSearch, setDebouncedSearch] = useState(search);

  useEffect(() => {
    const handler = setTimeout(() => {
      setDebouncedSearch(search);
    }, 500);
    return () => clearTimeout(handler);
  }, [search]);

  useEffect(() => {
    onSearch(debouncedSearch);
  }, [debouncedSearch, priceMin, priceMax]);

  return (
    <div className="flex flex-col gap-2 w-full max-w-md mb-6">
      <div className="flex gap-2 w-full">
        <Input
          className="flex-1"
          placeholder="Buscar productos..."
          value={search}
          onChange={(e) => setSearch(e.target.value)}
        />

        <Button
          variant="outline"
          size="sm"
          onClick={() => setShowAdvanced((prev) => !prev)}
        >
          Filtro avanzado
        </Button>
      </div>

      {showAdvanced && (
        <div className="flex gap-2 mt-2 w-full">
          <Input
            type="number"
            placeholder="Precio mínimo"
            value={priceMin}
            onChange={(e) =>
              setPriceMin(e.target.value === "" ? "" : Number(e.target.value))
            }
            className="flex-1"
          />
          <Input
            type="number"
            placeholder="Precio máximo"
            value={priceMax}
            onChange={(e) =>
              setPriceMax(e.target.value === "" ? "" : Number(e.target.value))
            }
            className="flex-1"
          />
        </div>
      )}
    </div>
  );
}
