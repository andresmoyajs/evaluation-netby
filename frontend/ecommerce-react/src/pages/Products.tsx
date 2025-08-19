import { useState } from "react";
import { keepPreviousData, useQuery } from "@tanstack/react-query";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { fetchProducts } from "@/actions/productsAction";
import type { ApiResponse } from "@/types/api";
import {
  EyeIcon,
  Loader2Icon,
  PencilIcon,
  Trash2Icon,
  ShoppingCartIcon,
  HandCoinsIcon,
  MoreVerticalIcon,
  CheckIcon,
  XIcon,
  PlusIcon,
  BookIcon,
} from "lucide-react";
import { useNavigate, useParams } from "react-router-dom";
import ProductSearch from "@/components/product/ProductSearch";
import PaginationModel from "@/components/layout/PaginationModel";
import { Button } from "@/components/ui/button";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import ProductDetailModal from "@/components/product/ProductDetailModal";
import type { Product } from "@/types/product";
import ProductStatusModal from "@/components/product/ProductStatusModal";
import ProductDeleteModal from "@/components/product/ProductDeleteModal";
import ProductCreateModal from "@/components/product/ProductCreateModal";
import ProductEditModal from "@/components/product/ProductEditModal";
import ProductBuyModal from "@/components/product/ProductBuyModal";
import ProductSellModal from "@/components/product/ProductSellModal";
import { getCategoryName } from "@/utilities/helper";
import ProductHistoryModal from "@/components/product/ProductHistoryModal";

export default function Products() {
  const { currentPage } = useParams();
  const page = currentPage ? parseInt(currentPage) : 1;
  const navigate = useNavigate();

  const [search, setSearch] = useState("");

  const [showAdvanced, setShowAdvanced] = useState(false);
  const [priceMin, setPriceMin] = useState<number | "">("");
  const [priceMax, setPriceMax] = useState<number | "">("");

  const [openDetails, setOpenDetails] = useState(false);
  const [openStatus, setOpenStatus] = useState(false);
  const [openDelete, setOpenDelete] = useState(false);
  const [openCreate, setOpenCreate] = useState(false);
  const [openEdit, setOpenEdit] = useState(false);
  const [openSell, setOpenSell] = useState(false);
  const [openBuy, setOpenBuy] = useState(false);
  const [openHistory, setOpenHistory] = useState(false);
  const [productItem, setProductItem] = useState<Product>({
    id: 0,
    name: "",
    description: "",
    price: 0,
    stock: 0,
    statusLabel: "",
    images: [],
  });

  if (!currentPage) {
    navigate(`/productos/${page}`);
  }

  const { isPending, data, isError, isFetching, isPlaceholderData } =
    useQuery<ApiResponse>({
      queryKey: ["products", page, search, priceMin, priceMax],
      queryFn: () =>
        fetchProducts({
          pageIndex: search ? 1 : page,
          pageSize: search ? 1000 : 3,
          search,
          priceMin,
          priceMax
        }),
      placeholderData: keepPreviousData,
    });

  const totalPages = data?.pageCount;

  const nextPage = () => {
    if (data?.pageIndex === data?.pageCount) return;
    navigate(`/productos/${page + 1}`);
  };

  const prevPage = () => {
    if (data?.pageIndex === 1) return;
    navigate(`/productos/${page - 1}`);
  };

  if (isPending) {
    return (
      <div className="flex items-center justify-center h-64">
        <Loader2Icon className="h-8 w-8 animate-spin text-primary" />
        <span className="ml-2 text-lg">Cargando productos...</span>
      </div>
    );
  }
  if (isError) return <p>Error al cargar productos</p>;

  return (
    <main className="p-6 space-y-6">
      <div className="flex items-center justify-between">
        <ProductSearch
          onSearch={setSearch}
          setShowAdvanced={setShowAdvanced}
          showAdvanced={showAdvanced}
          priceMin={priceMin}
          setPriceMin={setPriceMin}
          priceMax={priceMax}
          setPriceMax={setPriceMax}
        />

        <Button
          variant="outline"
          size="sm"
          className="flex items-center gap-2"
          onClick={() => {
            setOpenCreate(true);
          }}
        >
          <PlusIcon size={16} />
          Añadir
        </Button>
      </div>
      <Table>
        <TableHeader>
          <TableRow>
            <TableHead>Imagen</TableHead>
            <TableHead>Nombre</TableHead>
            <TableHead>Categoría</TableHead>
            <TableHead>Precio</TableHead>
            <TableHead>Stock</TableHead>
            <TableHead className="text-center">Estado</TableHead>
            <TableHead className="text-center">Acciones</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {data!.data.map((product) => (
            <TableRow key={product.id}>
              <TableCell>
                {product.images[0] ? (
                  <img
                    src={product.images[0].url}
                    alt={product.name}
                    className="w-16 h-16 object-cover rounded"
                  />
                ) : (
                  "No imagen"
                )}
              </TableCell>
              <TableCell>{product.name}</TableCell>
              <TableCell>
                {getCategoryName(parseInt(product.categoryId!))}
              </TableCell>
              <TableCell>${product.price}</TableCell>
              <TableCell>{product.stock}</TableCell>
              <TableCell>
                <div className="flex items-center justify-center">
                  <span
                    className={`h-3 w-3 rounded-full ${
                      product.statusLabel === "ACTIVO"
                        ? "bg-green-500"
                        : "bg-red-500"
                    }`}
                  />
                </div>
              </TableCell>

              <TableCell>
                <div className="flex justify-center gap-2">
                  {/* Botones directos */}
                  <Button
                    variant="outline"
                    size="sm"
                    onClick={() => {
                      setOpenBuy(true);
                      setProductItem(product);
                    }}
                    disabled={product.statusLabel !== "ACTIVO"}
                  >
                    <ShoppingCartIcon className="mr-1 h-4 w-4 text-purple-500" />
                    Comprar
                  </Button>
                  <Button
                    variant="outline"
                    size="sm"
                    onClick={() => {
                      setOpenSell(true);
                      setProductItem(product);
                    }}
                    disabled={product.statusLabel !== "ACTIVO"}
                  >
                    <HandCoinsIcon className="mr-1 h-4 w-4 text-yellow-500" />
                    Vender
                  </Button>

                  {/* Dropdown con más acciones */}
                  <DropdownMenu>
                    <DropdownMenuTrigger asChild>
                      <Button variant="ghost" size="icon">
                        <MoreVerticalIcon className="h-5 w-5" />
                      </Button>
                    </DropdownMenuTrigger>
                    <DropdownMenuContent align="end">
                      <DropdownMenuLabel>Acciones</DropdownMenuLabel>
                      <DropdownMenuSeparator />

                      <DropdownMenuItem
                        onClick={() => {
                          setOpenDetails(true);
                          setProductItem(product);
                        }}
                      >
                        <EyeIcon className="mr-2 h-4 w-4 text-blue-500" />
                        Ver
                      </DropdownMenuItem>

                      <DropdownMenuItem
                        onClick={() => {
                          setOpenHistory(true);
                          setProductItem(product);
                        }}
                      >
                        <BookIcon className="mr-2 h-4 w-4 text-yellow-500" />
                        Historial
                      </DropdownMenuItem>

                      <DropdownMenuItem
                        onClick={() => {
                          setOpenEdit(true);
                          setProductItem(product);
                        }}
                        disabled={product.statusLabel !== "ACTIVO"}
                      >
                        <PencilIcon className="mr-2 h-4 w-4 text-green-500" />
                        Editar
                      </DropdownMenuItem>

                      <DropdownMenuItem
                        onClick={() => {
                          setOpenStatus(true);
                          setProductItem(product);
                        }}
                      >
                        {product.statusLabel === "ACTIVO" ? (
                          <>
                            <XIcon className="mr-2 h-4 w-4 text-red-500" />
                            Desactivar
                          </>
                        ) : (
                          <>
                            <CheckIcon className="mr-2 h-4 w-4 text-green-500" />
                            Activar
                          </>
                        )}
                      </DropdownMenuItem>

                      <DropdownMenuItem
                        onClick={() => {
                          setOpenDelete(true);
                          setProductItem(product);
                        }}
                      >
                        <Trash2Icon className="mr-2 h-4 w-4 text-red-500" />
                        Eliminar
                      </DropdownMenuItem>
                    </DropdownMenuContent>
                  </DropdownMenu>
                </div>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>

      <PaginationModel
        search={search}
        pageIndex={data!.pageIndex}
        pageCount={data!.pageCount}
        page={page}
        totalPages={totalPages!}
        nextPage={nextPage}
        prevPage={prevPage}
        navigate={navigate}
      />

      <ProductCreateModal setOpen={setOpenCreate} open={openCreate} />

      <ProductEditModal
        setOpen={setOpenEdit}
        open={openEdit}
        product={productItem}
      />

      <ProductDetailModal
        setOpen={setOpenDetails}
        open={openDetails}
        product={productItem}
      />

      <ProductDeleteModal
        setOpen={setOpenDelete}
        open={openDelete}
        product={productItem}
      />

      <ProductStatusModal
        setOpen={setOpenStatus}
        open={openStatus}
        product={productItem}
      />

      <ProductBuyModal
        setOpen={setOpenBuy}
        open={openBuy}
        product={productItem}
      />

      <ProductSellModal
        setOpen={setOpenSell}
        open={openSell}
        product={productItem}
      />

      <ProductHistoryModal
        setOpen={setOpenHistory}
        open={openHistory}
        product={productItem}
      />
    </main>
  );
}
