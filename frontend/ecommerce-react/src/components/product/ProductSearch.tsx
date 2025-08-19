import { useState, useEffect } from "react";
import { Input } from "@/components/ui/input";

interface ProductSearchProps {
  onSearch: (search: string) => void;
}

export default function ProductSearch({ onSearch }: ProductSearchProps) {
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
  }, [debouncedSearch]);

  return (
    <div className="flex items-center gap-2 max-w-md mb-6">
      <Input
        placeholder="Buscar productos..."
        value={search}
        onChange={(e) => setSearch(e.target.value)}
      />

    </div>
  );
}
