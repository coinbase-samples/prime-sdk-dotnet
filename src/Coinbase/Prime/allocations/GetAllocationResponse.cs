/*
 * Copyright 2024-present Coinbase Global, Inc.
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

namespace Coinbase.Prime.Allocations
{
  public class GetAllocationResponse
  {
    public Allocation? Allocation { get; set; }

    public class GetAllocationResponseBuilder
    {
      private Allocation? _allocation;

      public GetAllocationResponseBuilder WithAllocation(Allocation allocation)
      {
        this._allocation = allocation;
        return this;
      }

      public GetAllocationResponse Build()
      {
        return new GetAllocationResponse()
        {
          Allocation = this._allocation
        };
      }
    }
  }
}